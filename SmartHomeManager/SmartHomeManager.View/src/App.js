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

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
          </header>
          <body>
              <TableContainer>
                  <Table variant='simple'>
                      <TableCaption>Imperial to metric conversion factors</TableCaption>
                      <Thead>
                          <Tr>
                              <Th>To convert</Th>
                              <Th>into</Th>
                              <Th isNumeric>multiply by</Th>
                          </Tr>
                      </Thead>
                      <Tbody>
                          <Tr>
                              <Td>inches</Td>
                              <Td>millimetres (mm)</Td>
                              <Td isNumeric>25.4</Td>
                          </Tr>
                          <Tr>
                              <Td>feet</Td>
                              <Td>centimetres (cm)</Td>
                              <Td isNumeric>30.48</Td>
                          </Tr>
                          <Tr>
                              <Td>yards</Td>
                              <Td>metres (m)</Td>
                              <Td isNumeric>0.91444</Td>
                          </Tr>
                      </Tbody>
                      <Tfoot>
                          <Tr>
                              <Th>To convert</Th>
                              <Th>into</Th>
                              <Th isNumeric>multiply by</Th>
                          </Tr>
                      </Tfoot>
                  </Table>
              </TableContainer>
          </body>
    </div>
  );
}

export default App;
