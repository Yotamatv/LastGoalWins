import React from "react";
import { PageNotFound } from "../utils/imageLinks";

export default function ErrorPage() {
  const handleClick = () => {
    window.location.href = "/Home";
  };
  return (
    <div className="error-page">
      <img src={PageNotFound} alt="PageNotFound" />
      <b>Oops! It looks like we cannot find what you were looking for.</b>
      <button onClick={handleClick} className="btn">
        Go To Home Page
      </button>
    </div>
  );
}
