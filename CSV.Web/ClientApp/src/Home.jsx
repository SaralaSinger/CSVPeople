import React, { useEffect, useState } from 'react';
import axios from 'axios';
import PersonRow from './PersonRow';

const Home = () => {

    const [people, setPeople] = useState([]);

    const getPeople = async () => {
        const { data } = await axios.get(`/api/peoplecsv/getall`);
        setPeople(data);
    }
    const onDelete = async () => {
        await axios.post(`/api/peoplecsv/delete`);
        setPeople([]);
    }

    useEffect(() => {
        getPeople();

    }, [])

    return (
        <div className="container" style={{ marginTop: 60 }}>
            <div className="row">
                <div className="col-md-6 offset-md-3 mt-5">
                    <button onClick={onDelete} className="btn btn-danger btn-lg w-100">Delete All</button>
                </div>
            </div>
            <table className="table table-hover table-striped table-bordered mt-5">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {people.map(p => <PersonRow key={p.id} person={p} />)}
                </tbody>
            </table>
        </div>

    )
}

export default Home;