import React, { useEffect } from 'react'
import { useSelector } from 'react-redux'
import "../style/MainContent.css"
import { Box, Container, Grid } from '@mui/material'
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import Contacts from '../components/Contacts';
import Chat from '../components/Chat';



function MainContent() {



    return (
        <Box className="box-main">
            <Container >
                <Grid container>
                    <Grid item xs={3}>
                        <Contacts />

                    </Grid>

                    <Grid item xs={9}>

                        <Chat />

                    </Grid>
                </Grid>
            </Container>
        </Box >

    )
}

export default MainContent