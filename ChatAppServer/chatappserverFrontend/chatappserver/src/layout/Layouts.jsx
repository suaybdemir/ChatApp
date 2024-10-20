import React from 'react'
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import MainContent from './MainContent';
import Navi from './Navi';
import { Routes, Route } from "react-router-dom"
import Login from '../components/Login';


function Layouts() {
    return (
        <Container maxWidth="xl" >
            <Grid container>
                <Grid item xs={12}>
                    <Navi />
                </Grid>
                <Grid item xs={12} sx={{ marginTop: "20px" }}>
                    <Routes>
                        <Route path="/login" element={<Login />} />
                        <Route path="/" element={<MainContent />} />

                    </Routes>

                </Grid>
            </Grid>

        </Container>
    )
}

export default Layouts