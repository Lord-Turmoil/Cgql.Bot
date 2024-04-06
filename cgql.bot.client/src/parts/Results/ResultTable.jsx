import './ResultTable.css'
/*
result: {
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
*/
export function ResultTable({ result }) {
    return (
        <div className="ResultTable">
            <div className='ResultTable__title'>
                <h3>{result.name}</h3>
                <span>(Execution time: <b>{result.milliseconds / 1000.0}</b>s)</span>
                <hr />
            </div>
            <div className="ResultTable__table">
                <table>
                    <tr>
                        {
                            result.result.headers.map((header, index) => {
                                return <th key={index}>{header}</th>
                            })
                        }
                    </tr>
                    {
                        result.result.rows.map((row, index) => {
                            return (
                                <tr key={index}>
                                    {
                                        row.map((cell, index) => {
                                            return <td key={index}>{cell}</td>
                                        })
                                    }
                                </tr>
                            )
                        })
                    }
                </table>
            </div>
        </div>
    );
}