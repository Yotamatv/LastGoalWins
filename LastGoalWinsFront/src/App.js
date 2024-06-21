import "./App.css";
import Layout from "./layout/Layout";
import Router from "./layout/Router";
import { BrowserRouter } from "react-router-dom";
import UserProvider from "./users/providers/UserProvider";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <UserProvider>
          <Layout>
            <Router />
          </Layout>
        </UserProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
