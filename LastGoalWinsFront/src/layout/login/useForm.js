import { useState } from "react";
import useUsers from "../../users/hooks/useUsers";

export default function useForm(initialValues, validate, type) {
  const [formData, setFormData] = useState(initialValues);
  const [errors, setErrors] = useState({});
  const { handleLogin, handleSignup } = useUsers();
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const validationErrors = validate(formData);
    if (Object.keys(validationErrors).length === 0) {
      console.log("Form data:", formData);
      type === "Login" ? handleLogin(formData) : handleSignup(formData);
      // Handle form submission
    } else {
      setErrors(validationErrors);
    }
  };

  return {
    formData,
    errors,
    handleChange,

    handleSubmit,
  };
}
