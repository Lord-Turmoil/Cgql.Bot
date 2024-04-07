import InflateBox from '~/components/InflateBox';
import { Helmet } from 'react-helmet';
import { useParams, useSearchParams, useNavigate } from 'react-router-dom';

import LogoNav from '~/parts/LogoNav/LogoNav';
import LanguageNav from '~/parts/LanguageNav/LanguageNav';
import { Footer } from '~/parts/Footer/Footer';
import { useEffect, useState } from 'react';
import { Results } from '~/parts/Results/Results';

import './ResultPage.css';
import { LoadCircle } from '~/components/LoadCircle/LoadCircle';
import api from '~/services/api';
import stall from '~/services/stall';

export function ResultPage() {
    const navigate = useNavigate();

    const [queryParams] = useSearchParams()
    const params = useParams();

    const [online, setOnline] = useState(false);
    const [data, setData] = useState();
    const [info, setInfo] = useState();

    useEffect(() => {
        if (data !== undefined) {
            setInfo({
                repository: data.task.repository.fullName,
                repositoryUrl: data.task.repository.htmlUrl,
                branch: data.task.ref.substring(data.task.ref.lastIndexOf('/') + 1),
                branchUrl: data.task.repository.htmlUrl + '/tree/' + data.task.ref.substring(data.task.ref.lastIndexOf('/') + 1),
                commit: data.task.commit.sha.substring(0, 10),
                commitUrl: data.task.repository.htmlUrl + '/commits/' + data.task.commit.sha,

            });
        }
    }, [data]);

    const [id, setId] = useState(params.id);
    const [key, setKey] = useState();

    const [error, setError] = useState();

    useEffect(() => {
        if (queryParams.has('key')) {
            setKey(queryParams.get('key'));
        } else {
            navigate('/404');
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
                <LogoNav data={info} online={online} />
                <hr />
                {data === undefined
                    ? (
                        error === undefined
                            ? <LoadCircle sx={{ height: "100px", marginTop: "10%" }} />
                            : renderError(error)
                    )
                    : (
                        <div>
                            <LanguageNav language='python' id={data.task.id} />
                            <div className='ResultPage__content'>
                                {renderOverview(data.result.queryCount, data.result.bugCount, data.task.duration)}
                            </div>
                            <Results results={data.result.results}></Results>
                        </div>
                    )
                }
                <Footer setOnline={setOnline} />
            </InflateBox>
        </div>
    )

    function formatMillisecondDuration(duration) {
        if (duration < 60000) {
            return <span><b>{duration / 1000}</b>s</span>;
        } else {
            const m = Math.floor(duration / 60000);
            const s = Math.floor(duration - m * 60000);
            return <span><b>{m}</b>m<b>{s}</b>s</span>;
        }
    }

    function formatSecondDuration(duration) {
        if (duration < 60) {
            return <span><b>{duration.toFixed(3)}</b>s</span>;
        } else {
            const m = Math.floor(duration / 60);
            const s = Math.floor(duration - m * 60);
            return <span><b>{m}</b>m<b>{s}</b>s</span>;
        }
    }

    function renderOverview(queryCount, bugCount, timeCost) {
        return (
            <div className='ResultPage__overview'>
                <h2>Overview</h2>
                {bugCount === 0
                    ? <p><b style={{ color: "greenyellow" }}>Congratulations!</b> No potential bugs found in your repository.</p>
                    : <p>We've ran <b>{queryCount}</b> queries on your repository in {formatSecondDuration(timeCost)}, and found <b style={{ color: "red" }}>{bugCount}</b> potential bugs.</p>
                }
            </div>
        )
    }

    function renderError(message) {
        return (
            <div className='ResultPage__error'>
                <h2>One or more errors occurred when we scan your repository!</h2>
                <p>{message}</p>
            </div>
        )
    }

    async function populateResult(id, key) {
        const dto = await stall(api.get('/api/Result/' + id, { key: key }), 2000);
        if (dto.meta.status === 0) {
            setData(dto.data);
        } else if (dto.meta.status === 404) {
            navigate('/404');
        } else {
            setError(dto.meta.message);
        }
    }
}