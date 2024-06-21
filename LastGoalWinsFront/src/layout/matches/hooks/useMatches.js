import { useCallback, useEffect, useState } from "react";
import {
  getEvents,
  getLineups,
  getMatchesByLeague,
  getTopScorers,
} from "../../../services/matchesApiService";

export default function useMatches() {
  const [matches, setMatches] = useState([]);
  const [events, setEvents] = useState([]);
  const [lineups, setLineups] = useState([]);
  const [topScorers, setTopScorers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchMatches = useCallback(async (leagueId, startDate, endDate) => {
    setLoading(true);
    setError(null);
    console.log("start " + startDate);
    if (!(leagueId && startDate && endDate)) {
      return [];
    }
    console.log(endDate);
    try {
      const fetchedMatches = await getMatchesByLeague(
        leagueId,
        startDate,
        endDate
      );
      setMatches(fetchedMatches);
      setLoading(false);
    } catch (err) {
      setError("Failed to fetch matches");
      console.error("Error fetching matches: ", err);
    } finally {
      setLoading(false);
    }
  }, []);
  const fetchLineups = useCallback(async (fixtureId) => {
    setLoading(true);
    setError(null);
    try {
      const fetchedLineups = await getLineups(fixtureId);
      if (fetchedLineups.length > 0) {
        setLineups((prevLineups) => [...prevLineups, ...fetchedLineups]);
      } else {
        console.log("Failed to fetch Lineups");
      }
      setLoading(false);
    } catch (err) {
      setError("Failed to fetch Lineups");
      console.error("Error fetching Lineups: ", err);
    } finally {
      setLoading(false);
    }
  }, []);
  const fetchEvents = useCallback(async (fixtureId) => {
    setLoading(true);
    setError(null);
    try {
      const fetchedEvents = await getEvents(fixtureId);
      if (fetchedEvents.length > 0) {
        setEvents((prevEvents) => [...prevEvents, ...fetchedEvents]);
      } else {
        console.log("Failed to fetch Events");
      }
      setLoading(false);
    } catch (err) {
      setError("Failed to fetch Events");
      console.error("Error fetching Events: ", err);
    } finally {
      setLoading(false);
    }
  }, []);
  const fetchTopScorers = useCallback(async (leagueId) => {
    setLoading(true);
    setError(null);
    try {
      const fetchedTopScorers = await getTopScorers(leagueId);
      if (fetchedTopScorers.length > 0) {
        setTopScorers((prevEvents) => [...prevEvents, ...fetchedTopScorers]);
      } else {
        console.log("Failed to fetch TopScorers");
      }
      setLoading(false);
    } catch (err) {
      setError("Failed to fetch TopScorers");
      console.error("Error fetching TopScorers: ", err);
    } finally {
      setLoading(false);
    }
  }, []);

  return {
    matches,
    loading,
    error,
    lineups,
    topScorers,
    setEvents,
    events,
    setLineups,
    fetchMatches,

    fetchLineups,
    fetchEvents,
    fetchTopScorers,
  };
}
