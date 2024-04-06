import InflateBox from '~/components/InflateBox';
import { Helmet } from 'react-helmet';


import LogoNav from '~/parts/LogoNav/LogoNav';
import LanguageNav from '~/parts/LanguageNav/LanguageNav';
import { Footer } from '~/parts/Footer/Footer';
import { useState } from 'react';
import { Results } from '~/parts/Results/Results';

import './ResultPage.css';
import { LoadCircle } from '~/components/LoadCircle/LoadCircle';

export function ResultPage() {
    const [online, setOnline] = useState(false);
    const [result, setResult] = useState({});

    return (
        <div className='ResultPage'>
            <Helmet>
                <title>Scan Result</title>
            </Helmet>
            <InflateBox overflow={true}>
                <LogoNav online={online} />
                <hr />
                {result === undefined
                    ? <LoadCircle sx={{ height: "100px", marginTop: "10%" }} />
                    : result === null ? <h1>404 Not Found</h1>
                        : <div>
                            <LanguageNav language='python' id='1' />
                            <div className='ResultPage__content'>
                                {renderOverview(10, 3, 1234)}
                            </div>
                            <Results results={result}></Results>
                        </div>
                }
                <Footer setOnline={setOnline} />
            </InflateBox>
        </div>
    )

    function renderOverview(queryCount, bugCount, timeCost) {
        return (
            <div className='ResultPage__overview'>
                <h2>Overview</h2>
                {bugCount === 0
                    ? <p><b style={{ color: "greenyellow" }}>Congratulations!</b> No potential bugs found in your repository.</p>
                    : <p>We've ran <b>{queryCount}</b> queries on your repository in <b>{timeCost / 1000.0}</b>s, and found <b style={{ color: "red" }}>{bugCount}</b> potential bugs.</p>
                }
            </div>
        )
    }
}