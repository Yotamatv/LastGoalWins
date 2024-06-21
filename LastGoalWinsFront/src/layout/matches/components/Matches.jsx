import React, { useCallback, useEffect, useState, useRef } from "react";
import Match from "./Match";
import useMatches from "../hooks/useMatches";

export default function Matches({
  selectedLeagueId,
  startDate,
  endDate,
  manualSearch,
}) {
  const { matches, loading, error, fetchMatches } = useMatches();
  const [selectedLeague, setSelectedLeague] = useState([]);

  const premierLeagueRef = useRef(null);
  const ligueOneRef = useRef(null);
  const serieARef = useRef(null);
  const bundesligaRef = useRef(null);
  const laLigaRef = useRef(null);
  const euroRef = useRef(null);

  const updateLeagues = useCallback(() => {
    premierLeagueRef.current = matches.filter(
      (match) => match.fixture.leagueid === 39
    );
    ligueOneRef.current = matches.filter(
      (match) => match.fixture.leagueid === 61
    );
    serieARef.current = matches.filter(
      (match) => match.fixture.leagueid === 135
    );
    bundesligaRef.current = matches.filter(
      (match) => match.fixture.leagueid === 78
    );
    laLigaRef.current = matches.filter(
      (match) => match.fixture.leagueid === 140
    );
    euroRef.current = matches.filter((match) => match.fixture.leagueid === 4);
  }, [matches]);

  useEffect(() => {
    console.log(startDate, endDate);
    console.log(manualSearch);
    console.log(selectedLeagueId);
    fetchMatches(selectedLeagueId, startDate, endDate);
    updateLeagues();
    const leagueMap = {
      39: premierLeagueRef.current,
      61: ligueOneRef.current,
      135: serieARef.current,
      78: bundesligaRef.current,
      140: laLigaRef.current,
      4: euroRef.current,
    };

    setSelectedLeague(leagueMap[selectedLeagueId]);
  }, [manualSearch]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div className="Matches">
      {Array.isArray(selectedLeague) &&
        selectedLeague.map((match, index) => (
          <Match key={index} className={`Match${index % 2}`} match={match} />
        ))}
    </div>
  );
}
