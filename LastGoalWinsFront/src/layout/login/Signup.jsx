import React, { useEffect } from "react";
import useForm from "./useForm";
import { useUser } from "../../users/providers/UserProvider";
import { Navigate } from "react-router-dom";
import ROUTES from "../routesModel";
const initialSignupValues = {
  email: "",
  password: "",
  firstName: "",
  lastName: "",
  profileImage: null,
};

const validateSignup = (values) => {
  const errors = {};
  if (!values.email) errors.email = "Email is required";
  if (!values.password) errors.password = "Password is required";
  if (!values.firstName) errors.firstName = "First name is required";
  if (!values.lastName) errors.lastName = "Last name is required";
  if (!values.profileImage)
    errors.profileImage = "Profile image URL is required";
  return errors;
};

export default function Signup() {
  const { formData, errors, handleChange, handleSubmit } = useForm(
    initialSignupValues,
    validateSignup,
    "Signup"
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

        <div className="form-group">
          <label htmlFor="firstName">First Name</label>
          <input
            type="text"
            id="firstName"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
          />
          {errors.firstName && (
            <span className="error">{errors.firstName}</span>
          )}
        </div>

        <div className="form-group">
          <label htmlFor="lastName">Last Name</label>
          <input
            type="text"
            id="lastName"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
          />
          {errors.lastName && <span className="error">{errors.lastName}</span>}
        </div>

        <div className="form-group">
          <label htmlFor="profileImage">Profile Image URL</label>
          <input
            type="url"
            id="profileImage"
            name="profileImage"
            value={formData.profileImage}
            onChange={handleChange}
          />
          {errors.profileImage && (
            <span className="error">{errors.profileImage}</span>
          )}
        </div>
        <button type="submit" className="btn">
          Sign Up
        </button>
      </form>
    </div>
  );
}
