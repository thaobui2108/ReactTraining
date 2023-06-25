import { useState } from 'react'
import { QueryClient, QueryClientProvider, useQuery } from '@tanstack/react-query'
import TodoModalEdit from "./todo/TodoModalEdit";
import TodoItem from "./todo/TodoItem";
import Filter from './todo/Filter';
import './App.css'

let todoAddNew: Todo = {
    id: 0,
    title: '',
    completed: false
};
const queryClient = new QueryClient()

function Todo() {
    const [filter, setFilter] = useState('');
    const [isOpen, setStateModal] = useState(false);
    const [itemInModal, setItemInModal] = useState(todoAddNew);

    var url = `http://localhost:4000/todo/list?searchValue=${filter}`

    const { isLoading, error, data } = useQuery({
        queryKey: ['todo-list', filter],
        queryFn: (): Promise<Todo[]> =>
            fetch(url).then(
                (res) => res.json(),
            ),
    })

    if (isLoading) return 'Loading...'

    if (error) return 'An error has occurred: ' + error
    const onAddorEditTodoItem = (item: Todo) => {
        setItemInModal(item);
        setStateModal(true);
    }
    const toggle = () => {
        setStateModal(!isOpen);
    };
    const onSearch =(value: string)=>{
        setFilter(value);
    }
    
    const renderList = data?.map((item) => {
        return (<TodoItem key={item.id} item={item} handleOnEditItem={onAddorEditTodoItem} />)
    })
    return (
        <>
            <div className="todolist" >
                <div>
                    <Filter handelSearch={onSearch} />
                    <div>
                        <button onClick={() => onAddorEditTodoItem(todoAddNew)}>Add New</button>
                        <TodoModalEdit isOpen={isOpen} todo={itemInModal} toggle={toggle} ></TodoModalEdit>
                    </div>

                </div>
                <div className="game-board">
                    <table>
                        <tbody> {renderList}</tbody>
                    </table>
                </div>
            </div >
        </>
    )
}


function TodoView() {
    return (
        <QueryClientProvider client={queryClient}>
            <Todo />
        </QueryClientProvider>
    )
}
export default TodoView