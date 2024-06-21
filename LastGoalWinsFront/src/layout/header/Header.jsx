import React from "react";
import { Logo } from "../../utils/imageLinks";
import useUsers from "../../users/hooks/useUsers";
import { useUser } from "../../users/providers/UserProvider";
import ROUTES from "../routesModel";

export default function Header() {
  const handleLogin = () => {
    window.location.href = ROUTES.LOGIN;
  };
  const handleSignup = () => {
    window.location.href = ROUTES.SIGNUP;
  };
  const navToHome = () => {
    window.location.href = ROUTES.HOME;
  };
  const navToControlPanel = () => {
    window.location.href = ROUTES.USER_CONTROL_PANEL;
  };

  const { user } = useUser();
  const { handleLogout } = useUsers();
  user && console.log(user.isAdmin);
  return (
    <div className="Header">
      <div className="logo" onClick={navToHome}>
        <img src={Logo} alt="logo" />
        Last Goal Wins
      </div>
      <div className="nav-buttons">
        {user ? (
          <>
            Hello {user.first}!
            <button onClick={handleLogout} className="btn">
              Logout
            </button>
            {user.isAdmin && (
              <button onClick={navToControlPanel} className="btn">
                Control Panel
              </button>
            )}
          </>
        ) : (
          <>
            <button onClick={handleLogin} className="btn">
              Login
            </button>
            <button onClick={handleSignup} className="btn">
              Sign Up
            </button>
          </>
        )}
      </div>
    </div>
  );
}
