
import {
    useQueryClient,
    useMutation,
} from '@tanstack/react-query'

export default function TodoItem({ item, handleOnEditItem }: { item: Todo, handleOnEditItem: (todo: Todo)=> void }) {
    const handleChangeItemTodo = (e: React.ChangeEvent<HTMLInputElement>) => {
        var newItem = { ...item, completed: e.target.checked};
        var requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newItem)
        };
        updateTodoMutation.mutate(requestOptions)
    };
   
    const handleDeleteItemTodo = () => {
        var requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(item)
        };
        deleteTodoMutation.mutate(requestOptions);
       
    };
    const handleEditItemTodo = () => {
        var newItem = { ...item };
        handleOnEditItem(newItem);
    };
   

    const queryClient = useQueryClient()
    const deleteTodoMutation = useMutation({
        mutationFn: (requestOptions: any) =>
            fetch('http://localhost:4000/todo/delete', requestOptions)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                }),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['todo-list'] })
        },
    })
    const updateTodoMutation = useMutation({
        mutationFn: (requestOptions: any) =>
            fetch('http://localhost:4000/todo/update', requestOptions)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                }),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['todo-list'] })
        },
    })
    return (
        <tr>
            <td>
                <input
                    type="checkbox"
                    id="completed"
                    name="completed"
                    autoFocus
                    value={item.id}
                    checked={item.completed}
                    onChange={handleChangeItemTodo}
                />
            </td>
            <td>
                {item.title}
            </td>
            <td><button onClick={handleEditItemTodo}>Edit</button></td>
            <td><button onClick={handleDeleteItemTodo}>Delete</button></td>

        </tr>


    );
}