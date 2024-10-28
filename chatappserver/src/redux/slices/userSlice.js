import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from "axios"

const initialState = {
    user: null,
}

export const getUser = createAsyncThunk(
    'users/getUser',
    async (username) => {


        const response = await axios.post(`https://localhost:44335/api/Authentication/Login?name=` + username);

        return response.data;
    },
);


export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        logOut: (state) => {
            state.user = null
        }
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder.addCase(getUser.fulfilled, (state, action) => {
            state.user = action.payload
        })

    },
})

// Action creators are generated for each case reducer function
export const { logOut } = userSlice.actions

export default userSlice.reducer