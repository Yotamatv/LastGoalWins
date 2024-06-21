import React, { useEffect, useState } from "react";
import EventIcons from "../../../utils/imageLinks";

export default function Event({ event }) {
  const home = event.clubsInFixturesId % 2 === 0;
  const [assistOrDescInfo, setAssistOrDescInfo] = useState(0);
  useEffect(() => {
    setAssistOrDescInfo(
      event.details === "Goal cancelled" ? 2 : event.player2 ? 1 : 0
    );
  }, [event.type, event.player2]);
  const eventImageSrc =
    event.details === "Normal Goal"
      ? EventIcons.GOAL
      : event.details === "Penalty"
      ? EventIcons.PEN
      : event.details === "Missed Penalty"
      ? EventIcons.PEN_MISS
      : event.details === "Own Goal"
      ? EventIcons.OWN_GOAL
      : event.details === "Red Card"
      ? EventIcons.RED_CARD
      : event.details === "Yellow Card"
      ? EventIcons.YELLOW_CARD
      : event.details === "Goal cancelled"
      ? EventIcons.VAR
      : event.type === "subst"
      ? EventIcons.SUB
      : "";
  console.log(event);
  if (eventImageSrc === "") {
    return <></>;
  }
  return (
    <div className={`Event`}>
      {home ? (
        <div className="Home">
          <div className="Container">
            {event.player1}
            {assistOrDescInfo === 2 ? (
              <>
                <br />
                <p>{event.details}</p>
              </>
            ) : assistOrDescInfo === 1 ? (
              <>
                <br />
                <p>{event.player2}</p>
              </>
            ) : (
              <></>
            )}
          </div>
          <div className="Container">
            <img src={eventImageSrc} alt={event.details} />
          </div>
          <div className="Container">{event.timeElapsed}'</div>
          <div className="Container"></div>
          <div className="Container"></div>
        </div>
      ) : (
        <div className="Away">
          <div className="Container"></div>
          <div className="Container"></div>
          <div className="Container">{event.timeElapsed}'</div>
          <div className="Container">
            <img src={eventImageSrc} alt={event.details} />
          </div>
          <div className="Container">
            {event.player1}
            {assistOrDescInfo === 2 ? (
              <>
                <br />
                <p>{event.details}</p>
              </>
            ) : assistOrDescInfo === 1 ? (
              <>
                <br />
                <p>{event.player2}</p>
              </>
            ) : (
              <></>
            )}
          </div>
        </div>
      )}
    </div>
  );
}
