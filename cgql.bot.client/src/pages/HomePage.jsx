import InflateBox from '~/components/InflateBox';
import React, { useEffect, useState } from 'react';

import { Helmet } from 'react-helmet';

import './HomePage.css';
import api from '~/services/api';
import stall from '~/services/stall';

export default function HomePage() {
    const [error, setError] = useState();
    const [profile, setProfile] = useState();

    useEffect(() => {
        fetchServerProfile();
    }, []);

    const getProfile = () => error === undefined ?
        <span className='HomePage__status HomePage__status_load'><i className='dot'></i>Loading...</span> :
        error ? <span className='HomePage__status HomePage__status_error'><i className='dot'></i>Offline</span> :
            <span className='HomePage__status HomePage__status_ok'><i className='dot'></i>Online</span>

    useEffect(() => {
        setProfile(getProfile());
    }, [error]);

    return (
        <div className='HomePage'>
            <Helmet>
                <title>CodeGraphQL.Bot</title>
            </Helmet>
            <InflateBox>
                <div className='HomePage__wrapper'>
                    {/* Favicon */}
                    <div className='HomePage__logo_wrapper'>
                        <img alt='favicon' src='favicon.png'></img>
                    </div>

                    <div className='Homepage__content_wrapper'>
                        {/* Title */}
                        <div className='HomePage__title_wrapper'>
                            <h1>CodeGraphQL.Bot</h1>
                        </div>
                        {/* Description */}
                        <div className="Homepage__description_wrapper">
                            <p>Scan every push to your repository with a graph-based static analyzer</p>
                            <br />
                            <p>Supported language:&nbsp;
                                <img alt='python' src='https://profilinator.rishav.dev/skills-assets/python-original.svg'></img>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                Coming soon:&nbsp;
                                <img alt='c' src='https://profilinator.rishav.dev/skills-assets/c-original.svg'></img>
                                <img alt='cplusplus' src='https://profilinator.rishav.dev/skills-assets/cplusplus-original.svg'></img>
                                <img alt='java' src='https://profilinator.rishav.dev/skills-assets/java-original-wordmark.svg'></img></p>
                        </div>
                    </div>
                    {/* Footer */}
                    <div className='HomePage__footer_wrapper'>
                        <p>Server status: {profile}</p>
                        <hr />
                        <div>
                            <p>Homepage: <a href="https://www.gitlink.org.cn/softbot/10033" target='_blank'>GitLink</a></p>
                            <p>Powered by React + ASP.NET Core</p>
                        </div>
                    </div>
                </div>
            </InflateBox>
        </div>
    );

    async function fetchServerProfile() {
        const dto = await stall(api.get('/api/Status/Ping'));
        setError(dto.meta.status != 0);
    }
}