import React from "react";
import "./Spinner.css";
import { ClipLoader } from "react-spinners";

type Props = {
    isLoading?: boolean;
};

const Spinner = ({ isLoading = true }: Props) => {
    return (
        <div id="loading-spinner">
            <ClipLoader
                color="#000"
                loading={isLoading}
                size={90}
                aria-label="Loading Spinner"
                data-testid="loader"
            />
        </div>
    );
};

export default Spinner;