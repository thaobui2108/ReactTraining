
import { useState } from 'react'
export default function Filter({handelSearch}: {handelSearch: (value: string) => void}) {
    const [filter, setFilter] = useState('');
    const handleChangeFilter = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFilter(e.target.value);
    };
    return (
        <div>
            <input
                type="text"
                value={filter}
                placeholder="Search here"
                onChange={handleChangeFilter}
            />
            <button onClick={()=>handelSearch(filter)}>Search</button>
        </div>
    );
}