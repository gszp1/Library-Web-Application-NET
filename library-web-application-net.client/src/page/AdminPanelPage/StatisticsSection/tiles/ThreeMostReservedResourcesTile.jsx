import React, {useState, useEffect} from "react";
import '../StatisticsSectionStyles.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faRankingStar } from '@fortawesome/free-solid-svg-icons';
import axios from "axios";
import { BarChart } from "@mui/x-charts";

function ThreeMostReservedResources({setSection}) {
    const [resources, setResources] = useState({
        firstName: 'Not defined',
        firstCount: 0,
        secondName: 'Not defined',
        secondCount: 0,
        thirdName: 'Not defined',
        thirdCount: 0
    });
    const [error, setError] = useState(false);

    const names = [resources.firstName, resources.secondName, resources.thirdName];
    const values = [resources.firstCount, resources.secondCount, resources.thirdCount];
    const topResources = [];
    for (let i = 0; i < names.length; ++i) {
        topResources.push([names[i], values[i]]);
    }

    const fetchThreeMostReservedResources = async () => {
        const url = 'http://localhost:9090/api/statistics/reservations/top3';
        try {
            let response = await axios.get(url, {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('WebLibToken')}`
                }
            });
            setResources(response.data);
            setError(false);
        } catch (error) {
            if (error.response && error.response.status === 403) {
                setSection('Error');
            }
            setError(true);
        }
    }

    useEffect(() => {
        fetchThreeMostReservedResources();
    }, []);

    return (
        <div className='largeStatTile'>
        <h2>{"Three Most Reserved Resources "} <FontAwesomeIcon icon={faRankingStar} style={{height:'1.75rem'}}/></h2>
            {error ? (
                <p>Failed to fetch resources</p>
            ) : (
                <>
                <BarChart
                    xAxis={[{scaleType: 'band', data: names, label:'Title'}]}
                    series={[{id: 'reservations', data: values, label:'Reservations', color: 'orange'}]}
                />
                <div className='topResourcesList'>
                    {topResources.map((res, index) => (
                        <div key={index} style={{display:'flex', flexDirection:'row'}}>
                            <p className='topResourcesHeader'>{res[0] === '' ? ' ' :`${res[0]}: `}</p>
                            <p>{res[0] === '' ? ' ' : `${res[1]}`}</p>
                        </div>
                    ))}
                </div>
                </>
            ) }
        </div>
    );
}

export default ThreeMostReservedResources;