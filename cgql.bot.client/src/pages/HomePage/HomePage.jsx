import InflateBox from '~/components/InflateBox';
import React from 'react';

import { Helmet } from 'react-helmet';

import './HomePage.css';
import { Footer } from '~/parts/Footer/Footer';
import c_logo from '~/assets/img/c.svg';
import cpp_logo from '~/assets/img/cpp.svg';
import java_logo from '~/assets/img/java.svg';
import python_logo from '~/assets/img/python.svg';

export default function HomePage() {
    return (
        <div className='HomePage'>
            <Helmet>
                <title>CodeGraphQL.Bot</title>
            </Helmet>
            <InflateBox minimum="512">
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
                                <img alt='python' src={python_logo}></img>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                Coming soon:&nbsp;
                                <img alt='c' src={c_logo}></img>
                                <img alt='cpp' src={cpp_logo}></img>
                                <img alt='java' src={java_logo}></img></p>
                        </div>
                    </div>
                    <Footer />
                </div>
            </InflateBox>
        </div>
    );
}