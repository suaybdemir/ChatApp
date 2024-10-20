import React, { useEffect } from 'react'
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';

import "../style/navi.css"
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { logOut } from '../redux/slices/userSlice';



function Navi() {

    const user = useSelector((state) => state.user.user)


    const navigate = useNavigate()

    const dispatch = useDispatch()

    const navigateToLogin = () => {
        navigate("/login")
    }



    const handleLogOut = () => {

        dispatch(logOut())

        navigate("/login")

    }



    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar className='appbar' position="static" sx={{ backgroundColor: "#f7f7f7" }}>
                <Toolbar>

                    <Typography variant="h6" component="div" sx={{ flexGrow: 1, color: "black" }} >
                        <span onClick={() => navigate("/")} className='header'>ChatAppServer</span>
                    </Typography>
                    <div style={{ display: "flex" }}>

                        {
                            user ?
                                <Button sx={{ color: "black" }} onClick={() => handleLogOut()} color="inherit">Logout</Button>
                                :
                                <Button sx={{ color: "black" }} onClick={() => navigateToLogin()} color="inherit">Login</Button>
                        }


                    </div>

                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default Navi