import React from "react";
import { Box } from '@chakra-ui/react'
import {
    Slider,
    SliderTrack,
    SliderFilledTrack,
    SliderThumb,
    SliderMark,
} from '@chakra-ui/react'

export default function DeviceConfig() {
   

    return (
        <div>Set Limit for device usage here<br></br><br></br>
           <div>Light Type Device</div>
           <Slider aria-label='slider-ex-1' defaultValue={30}>
            <SliderTrack>
                <SliderFilledTrack />
            </SliderTrack>
            <SliderThumb />
            </Slider>
            <div>10&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 20&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 30&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 40&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 50&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 60&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 70&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 80&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;90</div>
            <br></br>
            <div>Dark Type Device</div>
            <Slider aria-label='slider-ex-1' defaultValue={30}>
                <SliderTrack>
                    <SliderFilledTrack />
                </SliderTrack>
                <SliderThumb />
            </Slider>
            <div>10&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 20&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 30&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 40&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 50&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 60&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 70&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 80&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;90</div>
        </div>
    );
}