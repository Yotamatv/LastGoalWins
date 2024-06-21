import { useState, useCallback, useMemo } from "react";
import {
  deleteUser,
  getUsersData,
  login,
  makeUserAdmin,
  signup,
} from "../services/usersApiService";
import {
  getUser,
  removeToken,
  setTokenInLocalStorage,
} from "../services/localStorageService";
import { useUser } from "../providers/UserProvider";
import { useNavigate } from "react-router-dom";
import ROUTES from "../../layout/routesModel";
import normalizeUser from "../helpers/normalizeUser";
import useAxios from "./useAxios";

const useUsers = () => {
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const navigate = useNavigate();
  const { user, setUser, setToken } = useUser();
  useAxios();

  const requestStatus = useCallback(
    (loading, errorMessage, user = null) => {
      setLoading(loading);
      setUser(user);
      setError(errorMessage);
    },
    [setUser, setLoading, setError]
  );

  const handleLogin = useCallback(
    async (user) => {
      try {
        const token = await login(user);
        setTokenInLocalStorage(token);
        setToken(token);
        const userFromLocalStorage = getUser();
        requestStatus(false, null, userFromLocalStorage);
        navigate(ROUTES.HOME);
      } catch (error) {
        requestStatus(false, error, null);
      }
    },
    [navigate, requestStatus, setToken]
  );

  const handleLogout = useCallback(() => {
    removeToken();
    setUser(null);
  }, [setUser]);

  const handleSignup = useCallback(
    async (userFromClient) => {
      try {
        const normalizedUser = normalizeUser(userFromClient);
        await signup(normalizedUser);
        await handleLogin({
          email: userFromClient.email,
          password: userFromClient.password,
        });
      } catch (error) {
        requestStatus(false, error, null);
      }
    },
    [requestStatus, handleLogin]
  );

  const handleGetUsers = useCallback(async () => {
    try {
      const userData = await getUsersData();
      setLoading(false);
      setError(null);
      return userData;
    } catch (error) {
      setLoading(false);
      setError(error);
    }
  }, []);
  const handleDeleteUser = useCallback(async (id) => {
    try {
      const response = await deleteUser(id);
      setLoading(false);
      setError(null);
      return response;
    } catch (error) {
      setLoading(false);
      setError(error);
    }
  }, []);
  const handleMakeAdmin = useCallback(async (id) => {
    try {
      const response = await makeUserAdmin(id);
      setLoading(false);
      setError(null);
      return response;
    } catch (error) {
      setLoading(false);
      setError(error);
    }
  }, []);

  const value = useMemo(
    () => ({ isLoading, error, user }),
    [isLoading, error, user]
  );

  return {
    value,
    handleLogin,
    handleLogout,
    handleSignup,
    handleGetUsers,
    handleDeleteUser,
    handleMakeAdmin,
  };
};

export default useUsers;
