import Main from "./Main";
import Header from "./header/Header";

export default function Layout({ children }) {
  return (
    <div className="layout">
      <Header />
      <Main>{children}</Main>
      {/* <Footer /> */}
    </div>
  );
}
