import React from "react";
import useForm from "./useForm";
import { useUser } from "../../users/providers/UserProvider";
import { Navigate } from "react-router-dom";
import ROUTES from "../routesModel";

const initialLoginValues = {
  email: "",
  password: "",
};

const validateLogin = (values) => {
  const errors = {};
  if (!values.email) errors.email = "Email is required";
  if (!values.password) errors.password = "Password is required";
  return errors;
};

export default function Login() {
  const { formData, errors, handleChange, handleSubmit } = useForm(
    initialLoginValues,
    validateLogin,
    "Login"
  );
  const { user } = useUser();
  if (user) return <Navigate replace to={ROUTES.HOME} />;
  return (
    <div className="Login">
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="email">Email</label>
          <input
            type="email"
            id="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
          />
          {errors.email && <span className="error">{errors.email}</span>}
        </div>

        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            id="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
          />
          {errors.password && <span className="error">{errors.password}</span>}
        </div>

        <button type="submit" className="btn">
          Login
        </button>
      </form>
    </div>
  );
}
