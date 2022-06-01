import React from 'react';
import { useTranslation } from 'react-i18next';

function MazeGeneration() {
    const {t} = useTranslation();

    return (
        <div>
            <h2 className="main-headers">{t("generationTitle")}</h2>
            <div className="textBlocks">{t("generationText")}</div>
        </div>
    );
}

export default MazeGeneration;