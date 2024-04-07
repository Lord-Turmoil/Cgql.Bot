import React from 'react';


import './HomePage.css';
import c_logo from '~/assets/img/c.svg';
import cpp_logo from '~/assets/img/cpp.svg';
import java_logo from '~/assets/img/java.svg';
import python_logo from '~/assets/img/python.svg';
import HomeFrame from '~/parts/HomeFrame/HomeFrame';

export default function HomePage() {
    return (
        <HomeFrame>
            <p>Scan every push to your repository with a graph-based static analyzer</p>
            <br />
            <p>Supported language:&nbsp;
                <img alt='python' src={python_logo}></img>
                &nbsp;&nbsp;&nbsp;&nbsp;
                Coming soon:&nbsp;
                <img alt='c' src={c_logo}></img>
                <img alt='cpp' src={cpp_logo}></img>
                <img alt='java' src={java_logo}></img></p>
        </HomeFrame>
    );
}