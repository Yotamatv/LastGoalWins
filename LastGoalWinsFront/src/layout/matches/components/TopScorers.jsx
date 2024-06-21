import React, { useEffect } from "react";
import useMatches from "../hooks/useMatches";

export default function TopScorers({ selectedLeagueId }) {
  const { fetchTopScorers, topScorers } = useMatches();

  useEffect(() => {
    const fetchData = (selectedLeagueId) => {
      fetchTopScorers(selectedLeagueId);
    };
    fetchData(selectedLeagueId);
  }, [selectedLeagueId, fetchTopScorers]);

  return (
    <div className="TopScorers">
      <ol>
        {topScorers
          .filter((player) => player.leagueid === selectedLeagueId)
          .slice(0, 10)
          .map((player, index) => (
            <li key={index} className="top-scorer-item">
              <span className="player-name">{player.name}</span>
              <img className="club-logo" src={player.clubLogo} alt="club" />
              <span className="player-stats">
                <span className="goals">{player.goals} goals</span>
                <span className="assists">{player.assists} assists</span>
              </span>
            </li>
          ))}
      </ol>
    </div>
  );
}
