import React, { useEffect, useState } from "react";
import { normalizeDate } from "../../../utils/dateNormalize";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import { IconButton } from "@mui/material";
import useMatches from "../hooks/useMatches";
import Events from "./Events";

export default function Match({ className, match, leagueName }) {
  const fixtureId = match.fixture.id;
  const isFinished = match.fixture.status === "FT";
  const [isExpanded, setIsExpanded] = useState(false);
  const [matchEvents, setMatchEvents] = useState();
  const { fetchLineups, lineups, events, fetchEvents } = useMatches();

  useEffect(() => {
    if (isExpanded) {
      getLineups();
      getEvents();
    }
  }, [isExpanded, events]);

  const getLineups = () => {
    const lineupsMap = lineups.map((lineup) =>
      lineup.filter(
        (row) =>
          row.clubsInFixturesId === fixtureId * 10 ||
          row.clubsInFixturesId === fixtureId * 10 + 1
      )
    );
    if (isExpanded & (lineupsMap.length === 0)) {
      fetchLineups(fixtureId);
    }
    //console.log(lineupsMap);
  };

  const getEvents = () => {
    const eventsMap = events.map((event) =>
      event.filter(
        (row) =>
          row.clubsInFixturesId === fixtureId * 10 ||
          row.clubsInFixturesId === fixtureId * 10 + 1
      )
    );
    if (isExpanded & (eventsMap.length === 0)) {
      fetchEvents(fixtureId);
    }
    setMatchEvents(eventsMap);
  };

  const score = isFinished
    ? match.away.goalsScored + " : " + match.home.goalsScored
    : " : ";

  const minute = match.fixture.status;
  const homeName = match.home.name;
  const awayName = match.away.name;
  const homeImage = match.home.logo;
  const awayImage = match.away.logo;
  normalizeDate(match.fixture.fixtureDate);

  return (
    <div className={className}>
      <div className="Match">
        <div className="Arrow"></div>
        <div className="Team">
          <img src={awayImage} alt={awayName}></img>
          <br />
          <p>{awayName}</p>
        </div>
        <div className="Score">
          <p>{leagueName}</p>
          {isFinished && <p>{minute}</p>}
          <h4>{score}</h4>
          <p>{normalizeDate(match.fixture.fixtureDate)}</p>
        </div>
        <div className="Team">
          <img src={homeImage} alt={homeName}></img>
          <br />
          <p>{homeName}</p>
        </div>
        <div className="Arrow">
          <IconButton
            color="inherit"
            onClick={() => {
              setIsExpanded(!isExpanded);
            }}
          >
            {isExpanded ? (
              <KeyboardArrowUpIcon fontSize="large" />
            ) : (
              <KeyboardArrowDownIcon fontSize="large" />
            )}
          </IconButton>
        </div>
      </div>
      <div className={`ExpandedView ${isExpanded ? "expanded" : ""}`}>
        {/* {console.log(matchEvents)} */}
        {isExpanded && <Events events={matchEvents} />}
      </div>
    </div>
  );
}
