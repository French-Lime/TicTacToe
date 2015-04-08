<?php
    if (isset($_POST['eventCommand'])) {
        if ($_POST['eventCommand'] == 'GetEventData') {
            sleep(2);
            echo 1234;
        }
    }
?>
