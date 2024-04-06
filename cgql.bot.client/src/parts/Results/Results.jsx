import { ResultTable } from './ResultTable';
import './Results.css'

import loadingImage from '~/assets/img/loading.svg'

/*
"results": [
    {
        "name": "FlaskDebug",
        "result": {
            "headers": [
                "app.run",
                ""
            ],
            "rows": [
                [
                    "(prod.py:46) app.run(debug=True)",
                    "Flask run with debug mode enabled"
                ],
                [
                    "(prod-2.py:46) app.run(debug=True)",
                    "Flask run with debug mode enabled"
                ]
            ],
            "columnCount": 2,
            "bugCount": 2
        },
        "milliseconds": 38
    }
]
*/
const RESULT = {
    "name": "FlaskDebug",
    "result": {
        "headers": [
            "app.run",
            "comment"
        ],
        "rows": [
            [
                "(prod.py:46) app.run(debug=True)",
                "Flask run with debug mode enabled"
            ],
            [
                "(prod-2.py:46) app.run(debug=True)",
                "Flask run with debug mode enabled"
            ]
        ],
        "columnCount": 2,
        "bugCount": 2
    },
    "milliseconds": 38
};
export function Results({ results }) {
    if (results === undefined) {
        return null;
    } else if (results.length === 0) {
        return null;
    } else {
        return (
            // <div className='Results'>
            //     {
            //         results.map((result, index) => {
            //             result.bugCount === 0 ? null : <ResultTable result={result} key={index} />
            //         })
            //     }
            // </div>
            <div className='Results'>
                {
                    [RESULT, RESULT, RESULT].map((r, index) => {
                        return <ResultTable result={r} key={index} />
                    })
                }
            </div>
        );
    }
}