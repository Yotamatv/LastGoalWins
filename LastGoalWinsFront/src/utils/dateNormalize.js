export const normalizeDate = (date) => {
  const dateObj = new Date(date);

  // Extract the date and time components
  const day = dateObj.getUTCDate().toString().padStart(2, "0"); // Pad with zeros
  const month = (dateObj.getUTCMonth() + 1).toString().padStart(2, "0"); // Pad with zeros
  const year = (dateObj.getUTCFullYear() % 100).toString().padStart(2, "0"); // Convert year to two-digit representation and pad with zeros
  const hours = dateObj.getUTCHours().toString().padStart(2, "0"); // Pad with zeros
  const minutes = dateObj.getUTCMinutes().toString().padStart(2, "0"); // Pad with zeros

  // Format the components
  const formattedDate = `${day}/${month}/${year}\n${hours}:${minutes}`;

  return formattedDate;
};
