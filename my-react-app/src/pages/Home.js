import * as React from 'react';
import Grid  from "@mui/material/Unstable_Grid2";
import { styled } from '@mui/material/styles';
import Paper from '@mui/material/Paper';
import SideBar from '../components/SideBar';
import DashBoard from '../components/DashBoard';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
  }));
function Home() {
    return (

    <Grid container spacing={2}>
      <Grid Item xs={4} md={4}>
        {/* <Item><h1>Side Bar</h1></Item> */}
        <h1>Side Bar</h1>
        <SideBar />
      </Grid>
      <Grid Item xs={8} md={8}>
        {/* <Item><h1>Content</h1></Item> */}
        <h1>Content</h1>
        <DashBoard />
      </Grid>
    </Grid>
    );
}


export default Home;