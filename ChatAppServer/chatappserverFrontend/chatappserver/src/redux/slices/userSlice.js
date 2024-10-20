import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from "axios"

const initialState = {
    user: null,
}

export const getUser = createAsyncThunk(
    'users/getUser',
    async () => {
        const response = await axios.post("https://localhost:44335/api/Authentication/Login?name=enes")
        return response.data
    },
)

export const logOut = createAsyncThunk(
    'users/logOut',
    async () => {

        const response = await axios.post("https://localhost:44335/api/Authentication/Logout?name=enes")
        return response.data

    },
)

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder.addCase(getUser.fulfilled, (state, action) => {
            state.user = action.payload
        })
        builder.addCase(logOut.fulfilled, (state, action) => {
            state.user = null
        })
    },
})

// Action creators are generated for each case reducer function
export const { } = userSlice.actions

export default userSlice.reducer