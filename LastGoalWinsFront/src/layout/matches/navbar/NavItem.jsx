import React from "react";
import { Link } from "react-router-dom";

export default function NavItem({ to, label, isPressed, onPress }) {
  return (
    <Link to={to} className="ComponentButtons">
      <div className="LeagueButton">
        <button
          className={`NavButton${isPressed ? "Pressed" : ""}`}
          onClick={onPress}
        >
          <h2>{label}</h2>
        </button>
      </div>
    </Link>
  );
}
