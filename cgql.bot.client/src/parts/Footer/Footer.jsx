import { useEffect, useState } from 'react';

import api from '~/services/api';
import stall from '~/services/stall';

import './Footer.css';

export function Footer() {
    const [error, setError] = useState();
    const [serverStatus, setServerStatus] = useState();

    useEffect(() => {
        fetchServerStatus();
    }, []);

    const getProfile = () => error === undefined ?
        <span className='Footer__status Footer__status_load'><i className='dot'></i>Loading...</span> :
        error ? <span className='Footer__status Footer__status_error'><i className='dot'></i>Offline</span> :
            <span className='Footer__status Footer__status_ok'><i className='dot'></i>Online</span>

    useEffect(() => {
        setServerStatus(getProfile());
    }, [error]);

    return (
        <div className='Footer'>
            <p>Server status: {serverStatus}</p>
            <hr />
            <div>
                <p>Homepage: <a href="https://www.gitlink.org.cn/softbot/10033" target='_blank'>GitLink</a></p>
                <p>Powered by React + ASP.NET Core</p>
            </div>
        </div>
    );

    async function fetchServerStatus() {
        const dto = await stall(api.get('/api/Status/Ping'));
        setError(dto.meta.status != 0);
    }
}