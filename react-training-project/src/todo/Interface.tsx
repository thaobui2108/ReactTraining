interface Todo {
    id: number;
    title: string;
    completed: boolean;
};

interface ModalType {
    isOpen: boolean;
    todo: Todo;
    toggle: () => void;
}

