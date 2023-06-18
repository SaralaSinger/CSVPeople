import React, { useState } from 'react';


const Generate = () => {
    const [amount, setAmount] = useState();

    const onGenerateClick = () => {
        window.location.href= `/api/peopleCSV/generate?amount=${amount}`;
    }


    return (
        <div className="container" style={{ marginTop: 60 }}>
            <div className="d-flex vh-100" style={{ marginTop: "-70px" }}>
                <div className="d-flex w-100 justify-content-center align-self-center">
                    <div className="row">
                        <input
                            type="text"
                            className="form-control-lg"
                            placeholder="Amount"
                            value={amount}
                            onChange={e => setAmount(e.target.value)}
                        />
                    </div>
                    <div className="row">
                        <div className="col-md-4 offset-md-2">
                            <button onClick={onGenerateClick} className="btn btn-primary btn-lg" fdprocessedid="o69rtn">
                                Generate
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    )

}

export default Generate;