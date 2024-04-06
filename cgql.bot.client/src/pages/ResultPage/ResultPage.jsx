import InflateBox from '~/components/InflateBox';
import { Helmet } from 'react-helmet';
import { useParams, useSearchParams } from 'react-router-dom';

import LogoNav from '~/parts/LogoNav/LogoNav';
import LanguageNav from '~/parts/LanguageNav/LanguageNav';
import { Footer } from '~/parts/Footer/Footer';
import { useEffect, useState } from 'react';
import { Results } from '~/parts/Results/Results';

import './ResultPage.css';
import { LoadCircle } from '~/components/LoadCircle/LoadCircle';
import api from '~/services/api';

export function ResultPage() {
    const [queryParams] = useSearchParams()
    const params = useParams();

    const [online, setOnline] = useState(false);
    const [result, setResult] = useState();

    const [id, setId] = useState(params.id);
    const [key, setKey] = useState();

    const [error, setError] = useState();

    useEffect(() => {
        if (queryParams.has('key')) {
            setKey(queryParams.get('key'));
        } else {
            setError("Missing key in query parameters.");
        }
    }, [queryParams]);

    useEffect(() => {
        if (id !== undefined && key !== undefined) {
            populateResult(id, key);
        }
    }, [id, key]);

    console.log("🚀 > ResultPage > error:", error);

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
                            <LanguageNav language='python' id={id} />
                            <div className='ResultPage__content'>
                                {renderOverview(result.queryCount, result.bugCount, result.totalTime)}
                            </div>
                            <Results results={result.results}></Results>
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

    async function populateResult(id, key) {
        const dto = await api.get('/api/Result/' + id, { key: key });
        if (dto.meta.status === 0) {
            setResult(dto.data);
        } else {
            setError(dto.meta.message);
        }
    }
}