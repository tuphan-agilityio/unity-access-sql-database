<?php

    error_reporting(E_ALL);
    ini_set('display_errors', 1);


    $con = mysqli_connect("localhost","root","","unity_test_db");

    // Check connection
    if (mysqli_connect_errno())
    {
        echo "Failed to connect to MySQL: " . mysqli_connect_error();
    }
    
    $newScore = $con->real_escape_string(isset($_GET['newScore']) ? $_GET['newScore'] : '');
    $newName = $con->real_escape_string(isset($_GET['newName'])? $_GET['newName'] : '' );

    if ( ! empty( $newName ) && !empty($newScore) )
    {
        // Perform queries 
        $query = "INSERT INTO scores (name, score) VALUES ('$newName', '$newScore')";
        mysqli_query($con,$query);
    }

    mysqli_close($con);

?>
