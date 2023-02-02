import logo from './logo.svg';
import './App.css';
import {
  Table,
  Thead,
  Tbody,
  Tfoot,
  Tr,
  Th,
  Td,
  TableCaption,
  TableContainer,
} from '@chakra-ui/react'

import { WithSubnavigation } from './components/Header'

function App() {
  return (
    <div className="App">
        <body>
          <WithSubnavigation/>
        </body>
    </div>
  );
}

export default App;
