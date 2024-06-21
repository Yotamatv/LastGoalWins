import React from "react";
import NavItem from "./NavItem";
import ROUTES from "../../routesModel";

export default function Navbar({ onPress, pressedIndex }) {
  const handlePress = (index, leagueId) => {
    onPress(index, leagueId);
  };

  return (
    <div className="Navbar">
      <NavItem
        isPressed={pressedIndex === 0}
        onPress={() => handlePress(0, 4)}
        to={ROUTES.EURO}
        label={"Euro 2024"}
      />
      <NavItem
        isPressed={pressedIndex === 1}
        onPress={() => handlePress(1, 39)}
        to={ROUTES.EPL}
        label={"Premier League"}
      />
      <NavItem
        isPressed={pressedIndex === 2}
        onPress={() => handlePress(2, 61)}
        to={ROUTES.LIGUE_ONE}
        label={"Ligue 1"}
      />
      <NavItem
        isPressed={pressedIndex === 3}
        onPress={() => handlePress(3, 135)}
        to={ROUTES.SERIE_A}
        label={"Serie A"}
      />
      <NavItem
        isPressed={pressedIndex === 4}
        onPress={() => handlePress(4, 78)}
        to={ROUTES.BUNDESLIGA}
        label={"Bundesliga"}
      />
      <NavItem
        isPressed={pressedIndex === 5}
        onPress={() => handlePress(5, 140)}
        to={ROUTES.LA_LIGA}
        label={"La Liga"}
      />
    </div>
  );
}
