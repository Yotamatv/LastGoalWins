import React, { useEffect, useState } from "react";
import { useUser } from "../users/providers/UserProvider";
import useUsers from "../users/hooks/useUsers";

export default function ControlPanel() {
  const [users, setUsers] = useState([]);
  const { handleGetUsers, handleMakeAdmin, handleDeleteUser } = useUsers();
  const { user } = useUser();

  useEffect(() => {
    const fetchData = async () => {
      const data = await handleGetUsers();
      setUsers(data);
    };

    fetchData();
  }, [handleGetUsers]);

  if (!user || !user.isAdmin) return <b>You Can't Be Here Mate</b>;

  const makeAdmin = async (userId) => {
    await handleMakeAdmin(userId);
    // Refresh the user list after making admin
    const data = await handleGetUsers();
    setUsers(data);
  };

  const deleteUser = async (userId) => {
    await handleDeleteUser(userId);
    // Refresh the user list after deleting
    const data = await handleGetUsers();
    setUsers(data);
  };
  console.log(users);
  return (
    <div>
      <h1>USER MANAGEMENT CENTER</h1>
      <div className="user-list">
        {users.map((user) => (
          <div className="user-box" key={user.id}>
            <h2>{`${user.firstName} ${user.lastName}`}</h2>
            <img
              src={user.profilePictureUrl}
              alt={`${user.firstName} ${user.lastName}`}
              className="profile-pic"
            />
            <div className="user-details">
              <p>Id: {user.id}</p>
              <p>Email: {user.email}</p>
              <p>
                Is Admin:
                <input type="checkbox" checked={user.isAdmin} readOnly />
              </p>
            </div>
            <div className="user-actions">
              {!user.isAdmin && (
                <button onClick={() => makeAdmin(user.id)}>Make Admin</button>
              )}
              <button onClick={() => deleteUser(user.id)}>Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
