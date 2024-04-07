import HomeFrame from '~/parts/HomeFrame/HomeFrame'
import './Page404.css'
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

export default function Page404() {
    const navigate = useNavigate();
    const [counter, setCounter] = useState(5);

    useEffect(() => {
        if (counter === 0) {
            navigate('/');
        }
        
        const timer = setInterval(() => {
            setCounter(counter - 1);
        }, 1000);
        return () => clearInterval(timer);
    }, [counter]);

    return (
        <HomeFrame>
            <h2>404 Not Found</h2>
            <p>This is <b>not</b> the scan result you're looking for.</p>
            <p>Redirecting to the homepage in {counter}...</p>
        </HomeFrame>

    );
}