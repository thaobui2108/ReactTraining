import { useState,useEffect } from 'react'
import {
    useQueryClient,
    useMutation,
} from '@tanstack/react-query'
export default function TodoModalEdit(props: ModalType) {
    const [todoModel, setTodoModel] = useState(props.todo);

    useEffect(() => { setTodoModel(props.todo) }, [props.todo]);
    
    const changeTodo = (e:React.ChangeEvent<HTMLInputElement>) =>{
        var value = e.target.value;
        var newItem = { ...props.todo, title: value };
        setTodoModel(newItem);
    }
    const saveModel = () => {
        var requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(todoModel)
        };
        updateTodoMutation.mutate(requestOptions)
    }
    const queryClient = useQueryClient()
    const updateTodoMutation = useMutation({
        mutationFn: (requestOptions: any) =>
            fetch('http://localhost:4000/todo/update', requestOptions)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                }),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['todo-list'] })
            props.toggle();
        },
    })
    return (
        <>
            {props.isOpen && (
                <div className="modal-overlay">
                    <div className="modal-box">
                        <h5>Add or Edit </h5>
                        <input value={todoModel.title} onChange={changeTodo}></input>
                        <div>
                            <button onClick={props.toggle}>Close</button> <button onClick={saveModel}>Save</button>
                        </div>

                    </div>
                </div>
            )}
        </>
    );
}
