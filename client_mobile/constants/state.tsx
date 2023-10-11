import React, { createContext, useContext, useReducer, ReactNode } from 'react';

export interface IState {
  count: number;
  isLoggedIn: boolean;
  user: null | string;
}

const initialState: IState = {
  count: 0,
  isLoggedIn: false,
  user: null,
};

type Action = { type: 'increment' } | { type: 'decrement' } | { type: 'never' };

function reducer(state: IState, action: Action): IState {
  switch (action.type) {
    case 'increment':
      return { ...state, count: state.count + 1 };
    case 'decrement':
      return { ...state, count: state.count - 1 };
    default:
      throw new Error(`Unhandled action type: ${action?.type}`);
  }
}

interface StateContextProps {
  children: ReactNode;
}

export const StateContext = createContext<IState | undefined>(undefined);

export const StateProvider: React.FC<StateContextProps> = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  return (
    <StateContext.Provider value={state}>
      {children}
    </StateContext.Provider>
  );
};
