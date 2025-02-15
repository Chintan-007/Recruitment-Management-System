import { useState } from 'react'
import "react-toastify/dist/ReactToastify.css"
import './App.css'
import { ToastContainer } from 'react-toastify'
import { UserProvider } from './Context/useAuth'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <UserProvider>
        <ToastContainer/>
      </UserProvider>
    </>
  )
}

export default App
