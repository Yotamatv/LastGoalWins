import React, { useEffect, useState } from "react";
import Event from "./Event";

export default function Events({ events }) {
  const [isExpanded, setIsExpanded] = useState(false);
  useEffect(() => {
    setTimeout(() => {
      setIsExpanded(true);
    }, 1000);
    // return () => setIsExpanded(false);
  }, []);
  const orderedEvents =
    events &&
    events
      .flatMap((team) => team)
      .sort((a, b) => b.timeElapsed - a.timeElapsed);

  return (
    <>
      {isExpanded ? (
        <div className="Events">
          <div>
            {orderedEvents &&
              orderedEvents.map((_event) => (
                <Event key={_event.id} event={_event} />
              ))}
            <br />
            {console.log(isExpanded)}
            <br />
            <br />
          </div>
        </div>
      ) : (
        <></>
      )}
    </>
  );
}
