import InflateBox from '~/components/InflateBox';
import React, { useState } from 'react';

import { Helmet } from 'react-helmet';

import { Footer } from '~/parts/Footer/Footer';
import Logo from '~/parts/Logo/Logo';

import './HomeFrame.css'

export default function HomeFrame({ children }) {
    const [online, setOnline] = useState(false);

    return (
        <div className='HomeFrame'>
            <Helmet>
                <title>CodeGraphQL.Bot</title>
            </Helmet>
            <InflateBox minimum="512">
                <div className='HomeFrame__wrapper'>
                    {/* Favicon */}
                    <div style={{ height: "30%", marginTop: "2%" }}>
                        <Logo online={online} />
                    </div>

                    <div className='HomeFrame__content_wrapper'>
                        {/* Title */}
                        <div className='HomeFrame__title_wrapper'>
                            <h1>CodeGraphQL.Bot</h1>
                        </div>
                        {/* Description */}
                        <div className='HomeFrame__description_wrapper'>
                            {children}
                        </div>
                    </div>
                    <Footer setOnline={(o) => { setOnline(o); }} />
                </div>
            </InflateBox>
        </div>
    );
}