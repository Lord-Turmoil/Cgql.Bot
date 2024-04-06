import InflateBox from '~/components/InflateBox';
import React from 'react';

import { Helmet } from 'react-helmet';

import './HomePage.css';
import { Footer } from '~/parts/Footer/Footer';

export default function HomePage() {
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
                    <Footer />
                </div>
            </InflateBox>
        </div>
    );

    
}