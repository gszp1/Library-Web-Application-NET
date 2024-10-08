import React, {useState} from "react";
import './ResourcesSectionStyles.css';

function InstancesListEntry({instance, listId, withdrawInstance, updateInstance}) {
    const [updatedInstance, setUpdatedInstance] = useState({
        resourceId: instance.resourceId || 0,
        id: instance.id || 0,
        isReserved: !!instance.isReserved,
        instanceStatus: instance.instanceStatus || ''
    });
    
    const handleChange = (e) => {
        const { name, value } = e.target;
        const updatedValue = name === 'isReserved' ? value === 'true' : value;
        setUpdatedInstance(prev => ({
            ...prev,
            [name]: updatedValue
        }));
    };

    const handleUpdate = () => {
        const payload = {
            resourceId: updatedInstance.resourceId,
            id: updatedInstance.id,
            instanceStatus: updatedInstance.instanceStatus,
            isReserved: updatedInstance.isReserved
        }
        updateInstance(payload);
    }

    const handleWithdraw = () => {
        withdrawInstance(updatedInstance.id);
    }

    return (
        <tr>
            <td>{listId}</td>
            <td>
                <input
                    name='id'
                    value={updatedInstance.id}
                    onChange={handleChange}
                    readOnly
                />
            </td>
            <td>
                <select
                    name='isReserved'
                    value={updatedInstance.isReserved.toString()}
                    onChange={handleChange}
                >
                    <option value={"true"}>True</option>
                    <option value={"false"}>False</option>
                </select>
            </td>
            <td>
                <select
                    name='instanceStatus'
                    value={updatedInstance.instanceStatus}
                    onChange={handleChange}
                >
                    <option value="Active">Active</option>
                    <option value="Withdrawn">Withdrawn</option>
                    <option value="Awaiting_withdrawal">Awaiting withdrawal</option>
                </select>
            </td>
            <td>
                <button onClick = {handleWithdraw}>
                    Withdraw
                </button>
            </td>
            <td>
                <button onClick = {handleUpdate}>
                    Update
                </button>
            </td>
        </tr>
    );
}

export default InstancesListEntry;
