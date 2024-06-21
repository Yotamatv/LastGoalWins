import React, { useState } from "react";
import Navbar from "../layout/matches/navbar/Navbar";
import Matches from "../layout/matches/components/Matches";
import TopScorers from "../layout/matches/components/TopScorers";

export default function MainFrame() {
  const [pressedIndex, setPressedIndex] = useState(null);
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [activeComponent, setActiveComponent] = useState("Fixtures");
  const [selectedLeagueId, setSelectedLeagueId] = useState(null);
  const [searchPressed, setSearchPressed] = useState(false);

  const handlePress = (index, leagueId) => {
    setPressedIndex(index);
    setSelectedLeagueId(leagueId);
  };
  const handleSearchPress = () => {
    setSearchPressed(!searchPressed);
  };

  const handleComponentChange = (component) => {
    setActiveComponent(component);
  };

  return (
    <div className="MatchesPage">
      <div className="Navbar">
        <Navbar onPress={handlePress} pressedIndex={pressedIndex} />
      </div>

      <div className="DateInputs">
        <input
          type="date"
          value={startDate}
          onChange={(e) => setStartDate(e.target.value)}
        />
        <input
          type="date"
          value={endDate}
          onChange={(e) => setEndDate(e.target.value)}
        />
      </div>

      <div className="ComponentButtons">
        <button
          className={`NavButton${
            activeComponent === "Fixtures" ? "Pressed" : ""
          }`}
          onClick={() => handleComponentChange("Fixtures")}
        >
          Fixtures
        </button>
        <button
          className={`NavButton${
            activeComponent === "Tables" ? "Pressed" : ""
          }`}
          onClick={() => handleComponentChange("Tables")}
        >
          Tables
        </button>
        <button
          className={`NavButton${activeComponent === "Stats" ? "Pressed" : ""}`}
          onClick={() => handleComponentChange("Stats")}
        >
          Stats
        </button>
      </div>
      <div className="ComponentButtons">
        <button onClick={() => handleSearchPress()}>Search</button>
      </div>

      <div className="ComponentDisplay">
        {activeComponent === "Fixtures" && (
          <Matches
            selectedLeagueId={selectedLeagueId}
            startDate={startDate}
            endDate={endDate}
            manualSearch={searchPressed}
          />
        )}
        {/* {activeComponent === "Tables" && <LeagueTable />} */}
        {activeComponent === "Stats" && (
          <TopScorers
            selectedLeagueId={selectedLeagueId}
            manualUpdate={searchPressed}
          />
        )}
      </div>
    </div>
  );
}
