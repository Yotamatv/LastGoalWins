import React from "react";
import { Route, Routes } from "react-router-dom";
import Home from "../pages/Home";
import ROUTES from "./routesModel";
import Signup from "./login/Signup";
import Login from "./login/Login";
import ErrorPage from "../pages/ErrorPage";
import ControlPanel from "../pages/ControlPanel";
import MainFrame from "../pages/MainFrame";

export default function Router() {
  return (
    <Routes>
      <Route path={ROUTES.HOME} element={<Home />} />
      <Route path={ROUTES.LOGIN} element={<Login />} />
      <Route path={ROUTES.SIGNUP} element={<Signup />} />
      <Route path={ROUTES.EPL} element={<MainFrame />} />
      <Route path={ROUTES.LIGUE_ONE} element={<MainFrame />} />
      <Route path={ROUTES.SERIE_A} element={<MainFrame />} />
      <Route path={ROUTES.BUNDESLIGA} element={<MainFrame />} />
      <Route path={ROUTES.LA_LIGA} element={<MainFrame />} />
      <Route path={ROUTES.EURO} element={<MainFrame />} />
      <Route path={ROUTES.USER_CONTROL_PANEL} element={<ControlPanel />} />
      <Route path="*" element={<ErrorPage />} />
    </Routes>
  );
}
