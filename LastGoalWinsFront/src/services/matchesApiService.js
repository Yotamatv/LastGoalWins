import axios from "axios";
// import dummyFixturesData from "../DummyData/fixtures.json";
const apiUrl = "https://localhost:7223/FootballMatches";
export const getMatchesByLeague = async (leagueId, startDate, endDate) => {
  try {
    const response = await axios.get(
      `${apiUrl}/Fixtures/${leagueId}/${startDate}/${endDate}`
    );
    const data = response.data;
    // const responseTest = dummyFixturesData;
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
export const getLineups = async (fixtureId) => {
  try {
    const response = await axios.get(`${apiUrl}/Fixture/${fixtureId}/Lineups`);
    const data = response.data;
    // const responseTest = dummyFixturesData;
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
export const getEvents = async (fixtureId) => {
  try {
    const response = await axios.get(`${apiUrl}/Fixture/${fixtureId}/Events`);
    const data = response.data;
    // const responseTest = dummyFixturesData;
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
export const getAllMatches = async () => {
  try {
    const response = await axios.get(`${apiUrl}/FootballMatches/test`);
    const data = response.data;
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
export const getTopScorers = async (leagueId) => {
  try {
    const response = await axios.get(`${apiUrl}/TopScorers/${leagueId}`);
    const data = response.data;
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
