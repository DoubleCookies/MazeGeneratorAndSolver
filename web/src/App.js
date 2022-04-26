import './App.css';
import React from 'react';
import { Trans, useTranslation } from "react-i18next";
import i18n from "./i18n";
import GithubIcon from './GithubIcon';

function App() {
  const {t} = useTranslation();
  const changeLanguage = () => {
    let lng = 'en';
    if (i18n.language === 'en') {
      lng = 'ru';
    }
    i18n.changeLanguage(lng).then();
  }

  return (
      <div className="app">
        <header className="app-header">
          <div>Stats4wiki</div>
          <div>

            <span>┬┴┬┴┤ ͜ʖ ͡°) <button className="lang-button" aria-label="Switch language (en/ru)"
                                       title="Switch language (en/ru)" onClick={() => changeLanguage()}>
              🌎</button>├┬┴┬┴</span>
          </div>
        </header>
        <main className="main-content">
          <h2 className="main-headers">{t("aboutTitle")}</h2>
          <Trans i18nKey="aboutText">
            WIP!
          </Trans>
        </main>
        <footer className="footer-block">
          <div>
            <GithubIcon />
          </div>
          <div>2019-2022 <span role="img" aria-label="cookieEmoji">🍪</span></div>
        </footer>
      </div>
  );
}

export default App;
