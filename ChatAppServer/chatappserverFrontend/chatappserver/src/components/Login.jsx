import { Box, Button, Stack, TextField, Typography } from '@mui/material'
import React, { useEffect } from 'react'
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { useDispatch, useSelector } from 'react-redux';
import "../style/login.css"
import { getUser } from '../redux/slices/userSlice';
import { useNavigate } from "react-router-dom"
function Login() {

    const dispatch = useDispatch()

    const navigate = useNavigate()

    const handleLogin = () => {
        dispatch(getUser())
    }

    const user = useSelector((state) => state.user.user)

    useEffect(() => {
        if (user) {
            navigate("/")
        }
    }, [user])





    return (
        <Box className="login-box" sx={{ display: "flex", justifyContent: "center", alignItems: "center", padding: "20px", borderRadius: "20px" }}>
            <Stack sx={{ width: "75%" }} spacing={2}>


                <AccountCircleIcon sx={{ fontSize: "200px", alignSelf: "center" }} />

                <Typography sx={{ fontSize: "40px", alignSelf: "center" }}>Log in</Typography>


                <TextField className='login-input' sx={{ width: "100%", backgroundColor: "white" }} id="outlined-basic" label="Username" variant="filled" />

                <Button onClick={() => handleLogin()} variant="contained">ENTER</Button>

            </Stack>
        </Box>
    )
}

export default Login